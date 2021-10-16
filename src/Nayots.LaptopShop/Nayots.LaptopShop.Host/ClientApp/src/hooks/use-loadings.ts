import { useState } from 'react';

export const useLoadings = (): [
  { [key: number | string]: boolean },
  (key: number | string, isLoading: boolean) => void
] => {
  const [loadings, setLoadings] = useState<{ [key: number | string]: boolean }>(
    {}
  );

  const setLoading = (key: number | string, isLoading: boolean) => {
    setLoadings((x) => {
      return { ...x, [key]: isLoading };
    });
  };
  return [loadings, setLoading];
};
