import { Col, Container, Row } from "react-bootstrap";
import AppNavbar from "./components/layout/AppNavbar";
import Sidebar from "./components/layout/Sidebar";
import PageContainer from "./components/layout/PageContainer";

function App() {
  return (
    <>
      <AppNavbar />

      <Container fluid>
        <Row>
          <Col md={2}>
            <Sidebar />
          </Col>
          <Col md={10}>
            <PageContainer />
          </Col>
        </Row>
      </Container>
    </>
  );
}

export default App;
