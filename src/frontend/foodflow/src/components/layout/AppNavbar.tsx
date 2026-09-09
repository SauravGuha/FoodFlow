import { Container, Dropdown, Navbar } from "react-bootstrap";

export default function AppNavbar() {
  return (
    <Navbar bg="dark" variant="dark">
      <Container fluid>
        <Navbar.Brand>FoodFlow</Navbar.Brand>
        <Dropdown>
          <Navbar.Text>Admin</Navbar.Text>
        </Dropdown>
      </Container>
    </Navbar>
  );
}
