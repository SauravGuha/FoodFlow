import { createContext } from "react";

const LoaderContext = createContext<
  | {
      setLoading: (loading: boolean) => void;
      loaderStatus: boolean;
    }
  | undefined
>(undefined);

export default LoaderContext;
