import axios from 'axios';
import { useEffect, useState } from 'react';

import { ConfigurationConstants } from '../common/constants/configuration-constants';
import { UserInfo } from '../common/models/user-info';

export const useAuth = (): [
  string,
  UserInfo | undefined,
  (username: string, password: string) => Promise<boolean>,
  () => void
] => {
  const { Token_Key, Endpoints } = ConfigurationConstants;
  const [token, setToken] = useState("");
  const [userInfo, setUserInfo] = useState<UserInfo | undefined>(undefined);

  useEffect(() => {
    const savedToken = localStorage.getItem(Token_Key);
    if (savedToken) {
      setToken(savedToken);
    }
  }, [setToken, Token_Key]);

  useEffect(() => {
    axios.defaults.headers.common = { Authorization: `Bearer ${token}` };
  }, [token]);

  useEffect(() => {
    const getUserInfo = async () => {
      if (!token) {
        setUserInfo(undefined);
        return;
      }

      const userInfoResult = await axios.get<UserInfo>(Endpoints.auth);
      if (userInfoResult.status !== 200) {
        throw new Error(`Failed to retrieve user info`);
      }

      setUserInfo(userInfoResult.data);
    };
    getUserInfo();
  }, [token, setUserInfo, Endpoints.auth]);

  const tryLogin = async (
    username: string,
    password: string
  ): Promise<boolean> => {
    try {
      const authResult = await axios.post<string>(Endpoints.auth, {
        username,
        password,
      });
      setToken(authResult.data);
      return true;
    } catch (error) {
      console.error(`Cannot login: ${username}`, error);
      return false;
    }
  };

  const tryLogout = () => {
    try {
      localStorage.removeItem(Token_Key);
      setToken("");
      setUserInfo(undefined);
    } catch (error) {
      console.error("Error loging out", error);
      return false;
    }
  };

  return [token, userInfo, tryLogin, tryLogout];
};
