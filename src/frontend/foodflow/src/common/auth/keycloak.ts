import Keycloak from "keycloak-js";

const keycloak = new Keycloak({
  url: "http://localhost:10001",
  realm: "foodflowlocal",
  clientId: "foodflow-web",
});

export default keycloak;
