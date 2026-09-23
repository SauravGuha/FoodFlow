import { Spinner } from "react-bootstrap";

export default function Loading({ value }: { value: boolean }) {
  return value ? (
    <Spinner animation="border" variant="light" size="sm" />
  ) : null;
}
