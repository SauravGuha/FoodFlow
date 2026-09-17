import {
  createContext,
  useContext,
  useEffect,
  useState,
  type ReactNode,
} from "react";
import keycloak from "./keycloak";

interface AuthContextType {
  isAuthenticated: boolean;
  user: {
    id?: string;
    username?: string;
    name?: string;
    email?: string;
  } | null;
  login: () => void;
  register: () => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
  //setIsAuthenticated would become useful if we handle Keycloak events such as login/logout/token expiry without a full page reload.
  const [isAuthenticated, setIsAuthenticated] = useState(
    keycloak.authenticated ?? false,
  );

  const [user, setUser] = useState<AuthContextType["user"]>(null);

  useEffect(() => {
    if (!keycloak.authenticated) {
      return;
    }

    setUser({
      id: keycloak.subject,
      username: keycloak.tokenParsed?.preferred_username,
      name: keycloak.tokenParsed?.name,
      email: keycloak.tokenParsed?.email,
    });
  }, []);

  const login = () => {
    keycloak.login({
      redirectUri: window.location.origin,
    });
  };

  const register = () => {
    keycloak.register({
      redirectUri: window.location.origin,
    });
  };

  const logout = () => {
    keycloak.logout({
      redirectUri: window.location.origin,
    });
  };

  return (
    <AuthContext.Provider
      value={{
        isAuthenticated,
        user,
        login,
        register,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("useAuth must be used inside AuthProvider");
  }

  return context;
}
