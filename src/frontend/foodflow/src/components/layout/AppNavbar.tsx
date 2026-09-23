import { Container, Dropdown, Navbar } from "react-bootstrap";
import Loading from "../common/Loading";
import { useContext } from "react";
import LoaderContext from "../../common/utilities/appContext";
import { useAuth } from "../../common/auth/AuthContext";

export default function AppNavbar() {
  const { isAuthenticated, user, login, logout } = useAuth();

  const loaderContext = useContext(LoaderContext);
  const { loaderStatus } = loaderContext!;

  return (
    <Navbar bg="dark" variant="dark">
      <Container fluid>
        <Navbar.Brand>FoodFlow</Navbar.Brand>

        <Loading value={loaderStatus} />

        <div className="ms-auto">
          {isAuthenticated ? (
            <Dropdown align="end">
              <Dropdown.Toggle variant="dark" id="user-menu">
                {user?.name || user?.username || "Account"}
              </Dropdown.Toggle>

              <Dropdown.Menu>
                <Dropdown.Item onClick={logout}>Logout</Dropdown.Item>
              </Dropdown.Menu>
            </Dropdown>
          ) : (
            <button
              type="button"
              className="btn btn-outline-light"
              onClick={login}
            >
              Login
            </button>
          )}
        </div>
      </Container>
    </Navbar>
  );
}
