import { Button } from 'antd';
import React, { useContext, useMemo } from 'react';

import { DeleteOutlined, ShoppingCartOutlined } from '@ant-design/icons';

import { ProductType } from '../../../common/models/product-type';
import { IShopContext, ShopContext } from '../../../context/shop-context';

export interface IPurchaseButtonProps {
  productId: number;
  productType: ProductType;
  isLoading: boolean;
  onClick: (productId: number, action: "add" | "remove") => Promise<void>;
}

export const PurchaseButton: React.FC<IPurchaseButtonProps> = ({
  productId,
  productType,
  isLoading,
  onClick,
}): JSX.Element => {
  const { userIsLoggedIn, userCart } = useContext<IShopContext>(ShopContext);

  const itemInCart = useMemo(
    () =>
      userCart?.cartItems?.some((ci) => ci.productID === productId) ?? false,
    [userCart, productId]
  );
  const itemOfSameTypeInCart = useMemo(
    () =>
      userCart?.cartItems?.some(
        (ci) => ci.productType === productType && ci.productID !== productId
      ) ?? false,
    [userCart, productType, productId]
  );

  return (
    <>
      {!itemInCart && (
        <Button
          key="addToCart"
          type="primary"
          title={
            userIsLoggedIn
              ? itemOfSameTypeInCart
                ? "Product from same category already in cart"
                : "Add to cart"
              : "Login first"
          }
          disabled={!userIsLoggedIn || itemOfSameTypeInCart}
          icon={<ShoppingCartOutlined />}
          loading={isLoading}
          onClick={() => onClick(productId, "add")}
        >
          Add to Cart
        </Button>
      )}
      {itemInCart && userIsLoggedIn && (
        <Button
          key="removeFromCart"
          type="primary"
          danger
          title="Remove"
          icon={<DeleteOutlined />}
          loading={isLoading}
          onClick={() => onClick(productId, "remove")}
        >
          Remove
        </Button>
      )}
    </>
  );
};
