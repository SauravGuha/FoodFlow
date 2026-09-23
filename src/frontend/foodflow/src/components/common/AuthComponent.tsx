import { Outlet } from "react-router";
import { AuthProvider, useAuth } from "../../common/auth/AuthContext";

export default function AuthComponent() {
  const { isAuthenticated, login } = useAuth();
  if (!isAuthenticated) {
    login();
  }

  return (
    <AuthProvider>
      <Outlet />
    </AuthProvider>
  );
}
