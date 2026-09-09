import { Container, Dropdown, Navbar } from "react-bootstrap";
import Loading from "../common/Loading";
import { useContext } from "react";
import LoaderContext from "../../common/utilities/appContext";

export default function AppNavbar() {
  const loaderContext = useContext(LoaderContext);
  const { loaderStatus } = loaderContext!;

  return (
    <Navbar bg="dark" variant="dark">
      <Container fluid>
        <Navbar.Brand>FoodFlow</Navbar.Brand>
        <Loading value={loaderStatus} />
        <Dropdown>
          <Navbar.Text>Admin</Navbar.Text>
        </Dropdown>
      </Container>
    </Navbar>
  );
}
