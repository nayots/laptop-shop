import axios from 'axios';
import { useCallback, useEffect, useState } from 'react';

import { ConfigurationConstants } from '../common/constants/configuration-constants';
import { UserCart } from '../common/models/user-cart';

export const useCart = (
  userIsLoggedIn: boolean
): [UserCart | undefined, () => Promise<void>] => {
  const { Endpoints } = ConfigurationConstants;
  const [userCart, setUserCart] = useState<UserCart | undefined>(undefined);

  const loadCart = useCallback(async () => {
    try {
      if (userIsLoggedIn) {
        const cartResult = await axios.get<UserCart>(Endpoints.cart);
        setUserCart(cartResult.data);
      }
    } catch (error) {
      console.error("Could not load user cart", error);
    }
  }, [userIsLoggedIn, setUserCart, Endpoints]);

  useEffect(() => {
    loadCart();
  }, [userIsLoggedIn, loadCart]);

  return [userCart, loadCart];
};
