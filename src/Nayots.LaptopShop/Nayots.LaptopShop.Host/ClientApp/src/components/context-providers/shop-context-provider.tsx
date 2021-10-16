import React, { useCallback } from 'react';

import { IShopContext, ShopContext } from '../../context/shop-context';
import { useAuth } from '../../hooks/use-auth';
import { useCart } from '../../hooks/use-cart';

export const ShopContextProvider: React.FC<{}> = ({
  children,
}): JSX.Element => {
  const [, userInfo, tryLogin, tryLogout] = useAuth();
  const userIsLoggedIn = !!userInfo;

  const [cart, reload] = useCart(userIsLoggedIn);

  const login = useCallback(
    async (username: string, password: string) => {
      try {
        const succesfulLogin = await tryLogin(username, password);
        if (!succesfulLogin) {
          throw new Error(`Failure to login, check credentials`);
        }
      } catch (error) {
        console.error("Cannot login", error);
      }
    },
    [tryLogin]
  );

  const value: IShopContext = {
    userInfo,
    userIsLoggedIn,
    userCart: cart,
    reloadCart: reload,
    login,
    logout: tryLogout,
  };

  return <ShopContext.Provider value={value}>{children}</ShopContext.Provider>;
};
