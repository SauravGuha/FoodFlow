import { useContext, useEffect, useState } from "react";
import { Badge, Button, Card, Col, Container, Row } from "react-bootstrap";
import { useNavigate, useParams } from "react-router";
import type { Branch } from "../../common/types";
import LoaderContext from "../../common/utilities/appContext";
import { getRestuarantDetails } from "../../common/utilities/apiHelper";

export default function BranchList() {
  const { id } = useParams();
  const loaderContext = useContext(LoaderContext);
  const { setLoading, loaderStatus } = loaderContext!;
  const navigate = useNavigate();

  // State for branch and cuisine lists
  const [branches, setBranches] = useState<Branch[]>([]);

  if (!id) return <>Id not found...</>;

  useEffect(() => {
    setLoading(true);
    getRestuarantDetails(id)
      .then((res) => {
        // Populate branch and cuisine lists from the fetched restaurant data
        if (res.data?.branches) {
          setBranches(res.data.branches);
        }
      })
      .finally(() => setLoading(false));
  }, [id]);

  if (loaderStatus) return <>Loading...</>;

  function handleViewMore(id: string) {
    navigate(`/customer/menu/${id}`);
  }

  return (
    <Container fluid className="py-4">
      {/* Restaurant Header */}
      <div className="mb-4">
        <h3 className="mb-1">ABC Restaurant</h3>
        <p className="text-muted mb-0">Choose a branch to view the menu</p>
      </div>

      {/* Branches */}
      <Row xs={1} md={2} lg={3} className="g-4">
        {/* Branch */}
        {branches.map((b) => (
          <Col>
            <Card className="h-100 shadow-sm border-0">
              <Card.Body>
                <div className="d-flex justify-content-between align-items-start mb-3">
                  <h5 className="mb-0">{b.name}</h5>

                  <Badge bg="success">{b.status}</Badge>
                </div>

                <p className="text-muted mb-2">
                  {b.address.street}, {b.address.city}, {b.address.country}
                </p>

                <div className="small text-muted mb-3">Open until 10:30 PM</div>

                <Button
                  variant="primary"
                  className="w-100"
                  onClick={() => {
                    handleViewMore(b.id);
                  }}
                >
                  View Menu
                </Button>
              </Card.Body>
            </Card>
          </Col>
        ))}

        {/* Closed Branch */}
        <Col>
          <Card className="h-100 shadow-sm border-0">
            <Card.Body>
              <div className="d-flex justify-content-between align-items-start mb-3">
                <h5 className="mb-0">Park Street Branch</h5>

                <Badge bg="secondary">Closed</Badge>
              </div>

              <p className="text-muted mb-2">Park Street, Kolkata</p>

              <div className="small text-muted mb-3">Opens at 11:00 AM</div>

              <Button variant="outline-secondary" className="w-100" disabled>
                Currently Closed
              </Button>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}
