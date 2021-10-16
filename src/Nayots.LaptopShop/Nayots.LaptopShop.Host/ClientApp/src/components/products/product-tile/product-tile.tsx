import { Badge, Image } from 'antd';
import axios from 'axios';
import { debounce } from 'debounce';
import React, { useCallback, useContext } from 'react';

import { ConfigurationConstants } from '../../../common/constants/configuration-constants';
import { Product } from '../../../common/models/product';
import { IShopContext, ShopContext } from '../../../context/shop-context';
import { useLoadings } from '../../../hooks/use-loadings';
import { PurchaseButton } from '../purchase-button/purchase-button';
import styles from './styles.module.scss';

export interface IProductTileProps {
  product: Product;
}

export const ProductTile: React.FC<IProductTileProps> = ({
  product,
}): JSX.Element => {
  const { Images, DebounceTimes, Endpoints } = ConfigurationConstants;
  const { id, price, name, productType } = product;
  const { reloadCart } = useContext<IShopContext>(ShopContext);
  const [loadings, setLoading] = useLoadings();

  const onClick = useCallback(
    debounce(async (productId: number, action: "add" | "remove") => {
      try {
        setLoading(productId, true);
        action === "add"
          ? await axios.post(Endpoints.cart, { productId })
          : await axios.delete(Endpoints.cart, { data: { productId } });
        await reloadCart();
        setLoading(productId, false);
      } catch (error) {
        setLoading(productId, false);
        //TODO: handle
      }
    }, DebounceTimes.Normal),
    [setLoading, DebounceTimes, reloadCart]
  );

  return (
    <div className={styles.tileRoot}>
      <div className={styles.productName}>{name}</div>
      <Badge.Ribbon text={`${price.toFixed(2)} 💷`} color="purple">
        <div>
          <Image
            alt={Images.noImage}
            width={"10em"}
            src={Images[productType]}
          />
        </div>
      </Badge.Ribbon>
      <PurchaseButton
        onClick={onClick}
        isLoading={loadings[id]}
        productId={id}
        productType={productType}
      />
    </div>
  );
};
