import { Nav } from "react-bootstrap";

type SidebarProps = {
  onFeatureNavigation: (value: string) => void;
};

export default function Sidebar({ onFeatureNavigation }: SidebarProps) {
  return (
    <Nav className="flex-column">
      <Nav.Link
        onClick={() => {
          onFeatureNavigation("Dashboard");
        }}
      >
        Dashboard
      </Nav.Link>
      <Nav.Link
        onClick={() => {
          onFeatureNavigation("Restaurants");
        }}
      >
        Restaurants
      </Nav.Link>
      <Nav.Link
        onClick={() => {
          onFeatureNavigation("Branches");
        }}
      >
        Branches
      </Nav.Link>
      <Nav.Link
        onClick={() => {
          onFeatureNavigation("Cuisines");
        }}
      >
        Cuisines
      </Nav.Link>
      {/* <Nav.Link
        onClick={() => {
          onFeatureNavigation("Menus");
        }}
      >
        Menus
      </Nav.Link> */}
      <Nav.Link
        onClick={() => {
          onFeatureNavigation("Inventory");
        }}
      >
        Inventory
      </Nav.Link>
    </Nav>
  );
}
