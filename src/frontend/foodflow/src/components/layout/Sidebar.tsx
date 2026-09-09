import { Nav } from "react-bootstrap";

export default function Sidebar() {
  return (
    <Nav className="flex-column">
      <Nav.Link href="/">Dashboard</Nav.Link>
      <Nav.Link href="/restaurants">Restaurants</Nav.Link>
      <Nav.Link href="/branches">Branches</Nav.Link>
      <Nav.Link href="/cuisines">Cuisines</Nav.Link>
      <Nav.Link href="/menus">Menus</Nav.Link>
      <Nav.Link href="/inventory">Inventory</Nav.Link>
    </Nav>
  );
}
