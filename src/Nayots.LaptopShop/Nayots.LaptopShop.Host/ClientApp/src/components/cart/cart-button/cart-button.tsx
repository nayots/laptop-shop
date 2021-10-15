import { Avatar, Badge, Button, List, Modal } from 'antd';
import Menu from 'antd/lib/menu';
import axios from 'axios';
import { debounce } from 'debounce';
import React, { useCallback, useContext, useState } from 'react';

import { DeleteOutlined } from '@ant-design/icons';

import { ConfigurationConstants } from '../../../common/constants/configuration-constants';
import { IShopContext, ShopContext } from '../../../context/shop-context';
import styles from './styles.module.scss';

export interface ICartButtonProps {}

export const CartButton: React.FC<ICartButtonProps> = (props): JSX.Element => {
  const { DebounceTimes, Endpoints } = ConfigurationConstants;
  const { userInfo, userIsLoggedIn, userCart, reloadCart } =
    useContext<IShopContext>(ShopContext);
  const [showModal, setShowModal] = useState<boolean>(false);
  const [loadings, setLoadings] = useState<{ [key: number]: boolean }>({});

  const closeModal = useCallback(() => {
    setShowModal(false);
  }, [setShowModal]);
  const onCartCLick = useCallback(
    debounce(() => setShowModal(true), DebounceTimes.Normal),
    [setShowModal, DebounceTimes]
  );

  const refreshCart = useCallback(
    debounce(async () => {
      await reloadCart();
    }, DebounceTimes.VerySlow),
    [reloadCart, DebounceTimes]
  );

  const onRemoveFromCart = useCallback(
    async (productId: number) => {
      try {
        setLoadings((x) => {
          return { ...x, [productId]: true };
        });
        await axios.delete(Endpoints.cart, { data: { productId } });
        setLoadings((x) => {
          return { ...x, [productId]: false };
        });
        refreshCart();
      } catch (error) {}
    },
    [Endpoints, setLoadings, refreshCart]
  );

  return (
    <>
      <Menu.Item
        disabled={!userIsLoggedIn}
        title={userIsLoggedIn ? "View cart" : "Login first"}
        key="cart"
        onClick={onCartCLick}
      >
        <Badge count={userCart?.cartItems?.length ?? 0}>
          <span className={styles.cartText}>Cart 🛒</span>
        </Badge>
      </Menu.Item>
      <Modal
        title={`Hi ${userInfo?.username}, you have ${
          userCart?.cartItems.length ?? 0
        } items in your cart:`}
        visible={showModal}
        onOk={closeModal}
        onCancel={closeModal}
        footer={[
          <div key="price" className={styles.priceTotal}>
            Total prince: {(userCart?.totalPrice ?? 0).toFixed(2)} 💷
          </div>,
          <Button key="ok" onClick={closeModal}>
            Ok
          </Button>,
        ]}
      >
        <List
          itemLayout="horizontal"
          dataSource={userCart?.cartItems || []}
          renderItem={(item) => (
            <List.Item key={item.productId}>
              <List.Item.Meta
                avatar={
                  <Avatar src="https://res.cloudinary.com/fehbot/image/upload/v1634335563/laptop-shop/laptop.png" />
                }
                description={
                  <>
                    <span>{`${item.productName}, ${item.price.toFixed(
                      2
                    )} 💷`}</span>
                    <Button
                      className={styles.removeFromCartButton}
                      title="Remove from cart"
                      type="dashed"
                      danger
                      icon={<DeleteOutlined />}
                      loading={loadings[item.productId]}
                      onClick={() => onRemoveFromCart(item.productId)}
                    />
                  </>
                }
              />
            </List.Item>
          )}
        />
      </Modal>
    </>
  );
};
