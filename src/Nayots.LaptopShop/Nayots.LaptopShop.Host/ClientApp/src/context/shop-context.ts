import React from 'react';

import { UserCart } from '../common/models/user-cart';
import { UserInfo } from '../common/models/user-info';

export interface IShopContext {
  userIsLoggedIn: boolean;
  userInfo?: UserInfo;
  userCart?: UserCart;
  reloadCart: () => Promise<void>;
  login: (username: string, password: string) => Promise<void>;
  logout: () => void;
}

const defaultValue: IShopContext = {
  userIsLoggedIn: false,
  userInfo: undefined,
  userCart: undefined,
  reloadCart: () => Promise.resolve(),
  login: () => Promise.resolve(),
  logout: () => {},
};
export const ShopContext = React.createContext<IShopContext>(defaultValue);
