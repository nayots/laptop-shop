import { Layout, Menu } from 'antd';
import React from 'react';

import { CartButton } from '../../cart/cart-button/cart-button';
import { AuthButton } from '../../user/auth-button/auth-button';
import styles from './styles.module.scss';

const { Header: AntHeader } = Layout;

export interface IHeaderProps {}

export const Header: React.FC<IHeaderProps> = (props): JSX.Element => {
  return (
    <AntHeader>
      <div title="Computers and stuff" className={styles.logo}>
        Laptop Shop 💻
      </div>
      <Menu
        theme="dark"
        mode="horizontal"
        className={styles.menu}
        selectable={false}
      >
        <Menu.Item
          key="swagger"
          title="Explore the API with swagger"
          className={styles.swagger}
          onClick={() => window.open("/swagger", "_blank")}
        >
          Swagger 🔗
        </Menu.Item>
        <CartButton />
        <AuthButton />
      </Menu>
    </AntHeader>
  );
};
