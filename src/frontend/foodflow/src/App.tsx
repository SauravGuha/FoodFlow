import { Col, Container, Row } from "react-bootstrap";
import AppNavbar from "./components/layout/AppNavbar";
import Sidebar from "./components/layout/Sidebar";
import PageContainer from "./components/layout/PageContainer";
import { useState } from "react";
import LoaderContext from "./common/utilities/appContext";

function App() {
  const [feature, setFeature] = useState("DashBoard");
  const [showLoader, setShowLoader] = useState(false);

  function setLoaderUpdate(value: boolean) {
    setShowLoader(value);
  }

  function onFeatureNavigation(value: string) {
    if (!value) {
      setFeature("DashBoard");
    } else {
      setFeature(value);
    }
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
              <Sidebar onFeatureNavigation={onFeatureNavigation} />
            </Col>
            <Col md={10}>
              <PageContainer feature={feature} />
            </Col>
          </Row>
        </Container>
      </LoaderContext.Provider>
    </>
  );
}

export default App;
