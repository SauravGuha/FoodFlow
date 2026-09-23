import { Card, Col, Row } from "react-bootstrap";

export default function Dashboard() {
  return (
    <Row>
      <Col>
        <Card>
          <Card.Body>
            <Card.Title>Restaurants</Card.Title>
            <Card.Text>5</Card.Text>
          </Card.Body>
        </Card>
      </Col>

      <Col>
        <Card>
          <Card.Body>
            <Card.Title>Branches</Card.Title>
            <Card.Text>12</Card.Text>
          </Card.Body>
        </Card>
      </Col>
    </Row>
  );
}
