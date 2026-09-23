import { Button, Spinner } from "react-bootstrap";

export default function SaveButton({ submitting }: { submitting: boolean }) {
  return (
    <Button type="submit" variant="primary" disabled={submitting}>
      {submitting ? <Spinner size="sm" /> : <></>}
      Save
    </Button>
  );
}
