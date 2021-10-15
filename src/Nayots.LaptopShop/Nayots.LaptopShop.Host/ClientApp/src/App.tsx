import './App.scss';

import { Layout } from 'antd';
import React from 'react';

import { ProductType } from './common/models/product-type';
import { ShopContextProvider } from './components/context-providers/shop-context-provider';
import { Header } from './components/layout/header/header';
import { ProductCategory } from './components/products/product-category/product-category';

const { Content } = Layout;

const App: React.FC<{}> = () => {
  return (
    <div>
      <ShopContextProvider>
        <Header />
        <Content className={"mainContent"}>
          <ProductCategory productType={ProductType.Laptop} />
          <ProductCategory productType={ProductType.Ram} />
          <ProductCategory productType={ProductType.Storage} />
          <ProductCategory productType={ProductType.Color} />
        </Content>
      </ShopContextProvider>
    </div>
  );
};
export default App;
