import { Container, Dropdown, Navbar } from "react-bootstrap";
import Loading from "../common/Loading";
import { useContext } from "react";
import LoaderContext from "../../common/utilities/appContext";
import { useAuth } from "../../common/auth/AuthContext";

export default function AppNavbar() {
  const { isAuthenticated } = useAuth();
  const loaderContext = useContext(LoaderContext);
  const { loaderStatus } = loaderContext!;

  return (
    <Navbar bg="dark" variant="dark">
      <Container fluid>
        <Navbar.Brand>FoodFlow</Navbar.Brand>
        <Loading value={loaderStatus} />
        <Dropdown>
          <Navbar.Text>{isAuthenticated ? "Logout" : "Login"}</Navbar.Text>
        </Dropdown>
      </Container>
    </Navbar>
  );
}
