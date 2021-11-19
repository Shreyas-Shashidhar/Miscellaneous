import React, { Component } from 'react'
import TableHeader from "./tableHeader";
import TableBody from './tableBody';

class Table extends Component {
    constructor(props) {
        super(props);
        this.state = {  }
    }
    render() {     
        const { columns, sortColumn, onSort, data } = this.props;
        return (
            <table className="table">
                <TableHeader columns={columns} sortColumn={sortColumn} onSort={onSort}></TableHeader>
                <TableBody data= {data} columns={columns}></TableBody>
          </table>
        );
    }
}
 
export default Table;