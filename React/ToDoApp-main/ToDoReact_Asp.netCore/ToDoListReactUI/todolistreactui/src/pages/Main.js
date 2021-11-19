import React from "react";
import { Container, Row, Col, Table, Form, Button } from "react-bootstrap";
import AddTodo from "../components/AddTodo";
import ModalBox from "../components/ModalBox";
import axios from "axios";
axios.defaults.baseURL = 'http://localhost:37915/api';

export default class Main extends React.Component {
  state = {
    todos: [],
    modal: {
      show: false
    }
  };

  retrevieData = () =>{
    axios
    .get("/todoitems")
    .then(res => this.setState({ todos: res.data,  modal: { show: false } }))
    .catch(this.ErrorAlert);
  }
  componentDidMount() {
    this.retrevieData();
  };

 ErrorAlert = () => { 
     alert("Service unavailable");
 };

  labelStyle = todoCompleted => ({
    textDecoration: todoCompleted ? "line-through" : ""
  });

  addTodo = (title, priority) => {
    axios
      .post("/todoitems/", {
        title,
        priority:parseInt(priority) ,
        completed: false
      })
      .then(res => {
        this.retrevieData()
      }).catch(this.ErrorAlert);
  };

  changeTodo = todo => {
    axios.put('/todoitems/' + todo.id, {
      id: todo.id,
      title: todo.title,
      priority: todo.priority,
      completed: !todo.completed,
    })
    .then(res => {
      this.retrevieData()
    }).
    catch(this.ErrorAlert);
  };

  deleteTodo = id => {
    axios.delete('/todoitems/' + id)
    .then(res => {
      this.retrevieData();
    }).
    catch(this.ErrorAlert);
  };


  setModal = modal => {
    this.setState({ modal });
  };

  render = () => (
    <React.Fragment>
      <Container className="pt-3 px-0">
        <Row className="justify-content-center">
          <Col xl="7" lg="8" md="9" sm="10" xs="10">
            <AddTodo addTodo={this.addTodo} />
            <Table className="mt-3" striped bordered hover>
              <tbody>
                {this.state.todos.map(todo => (
                  <tr key={todo.id}>
                    <td className="d-flex align-items-center justify-content-between">
                      <Form.Check>
                        <Form.Check.Input
                          id={todo.id}
                          type="checkbox"
                          checked={todo.completed}
                          onChange={this.changeTodo.bind(this, todo)}
                        ></Form.Check.Input>
                        <Form.Check.Label
                          htmlFor={todo.id}
                          style={this.labelStyle(todo.completed)}
                        >
                          {todo.title}
                        </Form.Check.Label>
                      </Form.Check>
                      <Button
                        variant="danger"
                        onClick={this.setModal.bind(this, {
                          id: todo.id,
                          show: true
                        })}
                      >
                        Delete
                      </Button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </Col>
        </Row>
      </Container>
      <ModalBox
        modal={this.state.modal}
        setModal={this.setModal}
        deleteTodo={this.deleteTodo}
      />
    </React.Fragment>
  );
}
