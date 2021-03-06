import { notification, Spin } from 'antd';
import axios from 'axios';
import React, { useEffect, useState } from 'react';

import { ConfigurationConstants } from '../../../common/constants/configuration-constants';
import { ProductsResult } from '../../../common/models/product-result';
import { ProductType } from '../../../common/models/product-type';
import { State } from '../../../common/models/state';
import { ProductTile } from '../product-tile/product-tile';
import styles from './styles.module.scss';

export interface IProductCategoryProps {
  productType: ProductType;
}

export const ProductCategory: React.FC<IProductCategoryProps> = ({
  productType,
}): JSX.Element => {
  // const {userCart} = useContext<IShopContext>(ShopContext);
  const { Endpoints } = ConfigurationConstants;

  const [categoryProducts, setCategoryProducts] = useState<
    ProductsResult | undefined
  >(undefined);
  const [dataState, setDataState] = useState<State>(State.Idle);

  useEffect(() => {
    const getItemsForCategory = async () => {
      try {
        setDataState(State.Loading);
        const categoryItems = await axios.get<ProductsResult>(
          `${Endpoints.products}/all/${productType}`
        );
        setCategoryProducts(categoryItems.data);
        setDataState(State.Success);
      } catch (error) {
        notification.open({
          message: "Something went wrong",
          description: `Could not load category ${productType}, try again. 🙇‍♂️`,
        });
        setDataState(State.Failed);
      }
    };
    getItemsForCategory();
  }, [productType, setDataState, setCategoryProducts, Endpoints]);

  if (dataState === State.Failed || categoryProducts?.products?.length === 0) {
    return <></>;
  }

  return (
    <div className={styles.categoryRoot}>
      {dataState === State.Loading && <Spin />}
      {dataState === State.Success && categoryProducts && (
        <div>
          <div className={styles.categoryName}>
            {categoryProducts.productType}
          </div>
          <hr />
          <div className={styles.productsGrid}>
            {categoryProducts.products.map((p) => (
              <ProductTile key={p.id} product={p} />
            ))}
          </div>
        </div>
      )}
    </div>
  );
};
