import { Alert } from "react-bootstrap";

type ErrorMessageProps = {
  variant: "danger" | "success";
  message: string;
};

export default function ErrorMessage({ variant, message }: ErrorMessageProps) {
  return <Alert variant={variant}>{message}</Alert>;
}
