import { Nav } from "react-bootstrap";
import { Link } from "react-router";

export default function Sidebar() {
  return (
    <Nav className="flex-column">
      <Link to="/">Dashboard</Link>
      <Link to="/restaurants">Restaurants</Link>
      <Link to="/items">Items</Link>
    </Nav>
  );
}
