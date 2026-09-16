import { useContext, useEffect, useState } from "react";
import {
  Badge,
  Button,
  Card,
  Col,
  Form,
  InputGroup,
  Modal,
  Row,
  Table,
} from "react-bootstrap";
import { Link, useParams } from "react-router";
import type { BranchInventoryItem, Item } from "../../../common/types";
import LoaderContext from "../../../common/utilities/appContext";
import {
  addBranchStock,
  createBranchInventory,
  getBranchInventories,
  getItems,
  removeBranchStock,
} from "../../../common/utilities/apiHelper";

export default function InventoryList() {
  const { restaurantid, branchid } = useParams();
  const [branchInventories, setBranchInventories] = useState<
    BranchInventoryItem[]
  >([]);
  const [items, setItems] = useState<Item[]>([]);
  const [showAddModal, setShowAddModal] = useState(false);
  const [showStockModal, setShowStockModal] = useState(false);
  const [selectedInventory, setSelectedInventory] =
    useState<BranchInventoryItem | null>(null);
  const [stockQuantity, setStockQuantity] = useState(1);
  const [stockMode, setStockMode] = useState<"add" | "remove">("add");
  const [selectedItemId, setSelectedItemId] = useState("");
  const [price, setPrice] = useState("");
  const [initialQuantity, setInitialQuantity] = useState("0");

  const loaderContext = useContext(LoaderContext);
  const { setLoading, loaderStatus } = loaderContext!;

  const loadInventory = () => {
    if (!branchid) return Promise.resolve();
    return getBranchInventories(branchid).then((response) => {
      setBranchInventories(response.data);
    });
  };

  useEffect(() => {
    if (!branchid || !restaurantid) return;

    setLoading(true);
    Promise.all([
      getBranchInventories(branchid).then((response) =>
        setBranchInventories(response.data),
      ),
      getItems(restaurantid).then((response) => setItems(response.data)),
    ]).finally(() => setLoading(false));
  }, [branchid, restaurantid]);

  const inventoryItemIds = new Set(branchInventories.map((x) => x.itemId));
  const availableItems = items.filter(
    (item) => item.id && !inventoryItemIds.has(item.id),
  );

  const outOfStockCount = branchInventories.filter(
    (item) => item.quantity === 0,
  ).length;
  const totalStock = branchInventories.reduce(
    (total, item) => total + item.quantity,
    0,
  );

  const closeAddModal = () => {
    setShowAddModal(false);
    setSelectedItemId("");
    setPrice("");
    setInitialQuantity("0");
  };

  const handleAddItem = async (event: React.FormEvent) => {
    event.preventDefault();
    if (!branchid || !selectedItemId) return;

    setLoading(true);
    try {
      await createBranchInventory({
        itemId: selectedItemId,
        branchId: branchid,
        quantity: Number(initialQuantity),
        price: Number(price),
      });
      closeAddModal();
      await loadInventory();
    } finally {
      setLoading(false);
    }
  };

  const handleStockUpdate = async (event: React.FormEvent) => {
    event.preventDefault();
    if (!branchid || !selectedInventory || stockQuantity < 1) return;

    setLoading(true);
    try {
      const data = {
        itemId: selectedInventory.itemId,
        branchId: branchid,
        quantity: stockQuantity,
      };

      if (stockMode === "add") {
        await addBranchStock(data);
      } else {
        await removeBranchStock(data);
      }

      setShowStockModal(false);
      setSelectedInventory(null);
      await loadInventory();
    } finally {
      setLoading(false);
    }
  };

  const openStockModal = (
    inventory: BranchInventoryItem,
    mode: "add" | "remove",
  ) => {
    setSelectedInventory(inventory);
    setStockMode(mode);
    setStockQuantity(1);
    setShowStockModal(true);
  };

  if (!branchid || !restaurantid) return <>Branch not found...</>;
  if (loaderStatus) return <>Loading...</>;

  const branchName = branchInventories[0]?.branchName ?? "Branch Inventory";
  const restaurantName = branchInventories[0]?.restaurantName ?? "Restaurant";

  return (
    <Card>
      <Card.Body className="p-4">
        <div className="d-flex flex-wrap justify-content-between align-items-start gap-3 mb-4">
          <div>
            <div className="text-muted small mb-1">{restaurantName}</div>
            <h2 className="mb-1">{branchName}</h2>
            <div className="text-muted">Branch Inventory</div>
          </div>
          <div className="d-flex gap-2">
            <Link
              to={`/restaurants/${restaurantid}`}
              className="btn btn-outline-secondary"
            >
              Back to Restaurant
            </Link>
            <Button
              variant="primary"
              onClick={() => setShowAddModal(true)}
              disabled={availableItems.length === 0}
            >
              Add Item
            </Button>
          </div>
        </div>

        <Row className="g-3 mb-4">
          <Col md={4}>
            <Card className="h-100 border">
              <Card.Body>
                <div className="text-muted small">Inventory Items</div>
                <div className="fs-4 fw-semibold">
                  {branchInventories.length}
                </div>
              </Card.Body>
            </Card>
          </Col>
          <Col md={4}>
            <Card className="h-100 border">
              <Card.Body>
                <div className="text-muted small">Total Stock</div>
                <div className="fs-4 fw-semibold">{totalStock}</div>
              </Card.Body>
            </Card>
          </Col>
          <Col md={4}>
            <Card className="h-100 border">
              <Card.Body>
                <div className="text-muted small">Out of Stock</div>
                <div className="fs-4 fw-semibold text-danger">
                  {outOfStockCount}
                </div>
              </Card.Body>
            </Card>
          </Col>
        </Row>

        {branchInventories.length === 0 ? (
          <div className="text-center py-5 border rounded">
            <h5>No items in this branch</h5>
            <p className="text-muted mb-3">
              Add an item to start managing this branch inventory.
            </p>
            <Button
              variant="primary"
              onClick={() => setShowAddModal(true)}
              disabled={availableItems.length === 0}
            >
              Add Item
            </Button>
            {availableItems.length === 0 && items.length > 0 && (
              <div className="text-muted small mt-2">
                All restaurant items are already assigned to this branch.
              </div>
            )}
          </div>
        ) : (
          <Table striped bordered hover responsive align="center">
            <thead>
              <tr>
                <th>Item</th>
                <th>Cuisine</th>
                <th>Category</th>
                <th className="text-end">Price</th>
                <th className="text-end">Stock</th>
                <th>Action</th>
              </tr>
            </thead>
            <tbody>
              {branchInventories.map((inventory) => (
                <tr key={inventory.inventoryId}>
                  <td>
                    <div className="fw-semibold">{inventory.itemName}</div>
                    <div className="text-muted small">{inventory.sku}</div>
                  </td>
                  <td>{inventory.cuisineName}</td>
                  <td>
                    <Badge bg="secondary">{inventory.category}</Badge>
                  </td>
                  <td className="text-end">₹{inventory.price.toFixed(2)}</td>
                  <td className="text-end">
                    <div className="fw-semibold">{inventory.quantity}</div>
                    {inventory.quantity === 0 && (
                      <div className="text-danger small">Out of stock</div>
                    )}
                  </td>
                  <td>
                    <div className="d-flex flex-wrap gap-1">
                      <Button
                        size="sm"
                        variant="outline-primary"
                        onClick={() => openStockModal(inventory, "add")}
                      >
                        Add Stock
                      </Button>
                      <Button
                        size="sm"
                        variant="outline-danger"
                        disabled={inventory.quantity === 0}
                        onClick={() => openStockModal(inventory, "remove")}
                      >
                        Remove Stock
                      </Button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        )}
      </Card.Body>

      <Modal show={showAddModal} onHide={closeAddModal}>
        <Form onSubmit={handleAddItem}>
          <Modal.Header closeButton>
            <Modal.Title>Add Item to Branch</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form.Group className="mb-3">
              <Form.Label>Item</Form.Label>
              <Form.Select
                value={selectedItemId}
                onChange={(event) => setSelectedItemId(event.target.value)}
                required
              >
                <option value="">Select item</option>
                {availableItems.map((item) => (
                  <option key={item.id} value={item.id}>
                    {item.name} ({item.sku})
                  </option>
                ))}
              </Form.Select>
            </Form.Group>

            <Row>
              <Col>
                <Form.Group className="mb-3">
                  <Form.Label>Price</Form.Label>
                  <InputGroup>
                    <InputGroup.Text>₹</InputGroup.Text>
                    <Form.Control
                      type="number"
                      min="0"
                      step="0.01"
                      value={price}
                      onChange={(event) => setPrice(event.target.value)}
                      required
                    />
                  </InputGroup>
                </Form.Group>
              </Col>
              <Col>
                <Form.Group className="mb-3">
                  <Form.Label>Initial Quantity</Form.Label>
                  <Form.Control
                    type="number"
                    min="0"
                    step="1"
                    value={initialQuantity}
                    onChange={(event) => setInitialQuantity(event.target.value)}
                    required
                  />
                </Form.Group>
              </Col>
            </Row>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={closeAddModal}>
              Cancel
            </Button>
            <Button variant="primary" type="submit">
              Add Item
            </Button>
          </Modal.Footer>
        </Form>
      </Modal>

      <Modal show={showStockModal} onHide={() => setShowStockModal(false)}>
        <Form onSubmit={handleStockUpdate}>
          <Modal.Header closeButton>
            <Modal.Title>
              {stockMode === "add" ? "Add Stock" : "Remove Stock"}
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <div className="mb-3">
              <div className="fw-semibold">{selectedInventory?.itemName}</div>
              <div className="text-muted">
                Current stock: {selectedInventory?.quantity ?? 0}
              </div>
            </div>
            <Form.Group>
              <Form.Label>Quantity</Form.Label>
              <Form.Control
                type="number"
                min="1"
                step="1"
                value={stockQuantity}
                onChange={(event) =>
                  setStockQuantity(Number(event.target.value))
                }
                required
              />
            </Form.Group>
          </Modal.Body>
          <Modal.Footer>
            <Button
              variant="secondary"
              onClick={() => setShowStockModal(false)}
            >
              Cancel
            </Button>
            <Button
              variant={stockMode === "add" ? "primary" : "danger"}
              type="submit"
            >
              {stockMode === "add" ? "Add Stock" : "Remove Stock"}
            </Button>
          </Modal.Footer>
        </Form>
      </Modal>
    </Card>
  );
}
