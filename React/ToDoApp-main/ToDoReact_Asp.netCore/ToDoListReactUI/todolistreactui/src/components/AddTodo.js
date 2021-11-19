import React from "react";
import { InputGroup, FormControl, Button } from "react-bootstrap";

export default class AddTodo extends React.Component {
  state = {
    title: "",
    priority: 1
  };

  inputChange = e => this.setState({ [e.target.name]: e.target.value });

  addTodo = () => {
    if(this.state.title !==""){
      this.props.addTodo(this.state.title, this.state.priority);
      this.setState({ title: "" })
      this.setState({ priority: 1 });
    }
  };

  render = () => (
    <InputGroup>
      <FormControl
        name="title"
        placeholder="Add todo ..."
        value={this.state.title}
        onChange={this.inputChange} 
      />
       <InputGroup.Append>   
       <FormControl
         label = "priority"
          name ="priority"
          as="select"
          value={this.state.priority}
          onChange={this.inputChange}
        >
          <option value="1">Priority1</option>
          <option value="2">Priority2</option>
          <option value="3">Priority3</option>
          <option value="4">Priority4</option>
          <option value="5">Priority5</option>
        </FormControl>
      </InputGroup.Append>
      <InputGroup.Append>
        <Button variant="success" onClick={this.addTodo}>
          Add
        </Button>
      </InputGroup.Append>
    </InputGroup>
  );
}
