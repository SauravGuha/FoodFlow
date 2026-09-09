import { Button, Modal } from "react-bootstrap";

type ConfirmModalProps = {
  show: boolean;
};

export default function ConfirmModal({ show }: ConfirmModalProps) {
  return (
    <Modal show={show}>
      <Modal.Header>
        <Modal.Title>Confirm Delete</Modal.Title>
      </Modal.Header>

      <Modal.Body>Are you sure you want to delete this restaurant?</Modal.Body>

      <Modal.Footer>
        <Button variant="secondary">Cancel</Button>

        <Button variant="danger">Delete</Button>
      </Modal.Footer>
    </Modal>
  );
}
