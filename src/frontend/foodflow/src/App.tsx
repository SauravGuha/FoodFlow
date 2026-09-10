import { Col, Container, Row } from "react-bootstrap";
import AppNavbar from "./components/layout/AppNavbar";
import Sidebar from "./components/layout/Sidebar";
import { useState } from "react";
import LoaderContext from "./common/utilities/appContext";
import { Outlet } from "react-router";

function App() {
  const [showLoader, setShowLoader] = useState(false);

  function setLoaderUpdate(value: boolean) {
    setShowLoader(value);
  }

  return (
    <>
      <LoaderContext.Provider
        value={{ setLoading: setLoaderUpdate, loaderStatus: showLoader }}
      >
        <AppNavbar />

        <Container fluid>
          <Row>
            <Col md={2}>
              <Sidebar />
            </Col>
            <Col md={10}>
              <div className="page-container">
                <Outlet />
              </div>
            </Col>
          </Row>
        </Container>
      </LoaderContext.Provider>
    </>
  );
}

export default App;
