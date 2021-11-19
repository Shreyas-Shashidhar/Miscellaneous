import React from "react";
import { Modal, Button } from "react-bootstrap";

export default class ModalBox extends React.Component {
  
    render = () => (
        <Modal
        size="sm"
        show={this.props.modal.show}
        onHide={this.props.setModal.bind(this, { show: false })}
        centered
      >
        <Modal.Header closeButton>
          <Modal.Title>Delete todo</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p>Are you sure to delete this todo?</p>
        </Modal.Body>
        <Modal.Footer>
          <Button
            variant="secondary"
            onClick={this.props.setModal.bind(this, { show: false })}
          >
            Cancel
          </Button>
          <Button
            variant="danger"
            onClick={this.props.deleteTodo.bind(this, this.props.modal.id)}
          >
            Delete
          </Button>
        </Modal.Footer>
      </Modal>
    );
  }

