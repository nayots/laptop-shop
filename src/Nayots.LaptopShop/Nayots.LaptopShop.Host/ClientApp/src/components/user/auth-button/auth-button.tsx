import { Menu } from 'antd';
import { debounce } from 'debounce';
import React, { useCallback, useContext } from 'react';

import { ConfigurationConstants } from '../../../common/constants/configuration-constants';
import { IShopContext, ShopContext } from '../../../context/shop-context';

export interface IAuthButtonProps {}

export const AuthButton: React.FC<IAuthButtonProps> = (props): JSX.Element => {
  const { DemoCredentials, DebounceTimes } = ConfigurationConstants;
  const { userIsLoggedIn, userInfo, login, logout } =
    useContext<IShopContext>(ShopContext);
  const onLoginClick = useCallback(
    debounce(async () => {
      await login(DemoCredentials.username, DemoCredentials.password);
    }, DebounceTimes.Normal),
    [login, DemoCredentials, DebounceTimes]
  );

  const onLogoutClick = useCallback(
    debounce(() => {
      logout();
    }, DebounceTimes.Normal),
    [logout, DebounceTimes]
  );

  return (
    <>
      {!userIsLoggedIn && (
        <Menu.Item
          title="With Demo Creentials"
          key="login"
          onClick={onLoginClick}
        >
          Login 🔐
        </Menu.Item>
      )}
      {userIsLoggedIn && (
        <Menu.Item key="username">{userInfo?.username} 👤</Menu.Item>
      )}
      {userIsLoggedIn && (
        <Menu.Item key="logout" onClick={onLogoutClick}>
          Logout 🚪
        </Menu.Item>
      )}
    </>
  );
};
