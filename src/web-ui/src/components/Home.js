import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1>Currencies</h1>
        <h3>Features:</h3>
        <ol>
          <Link to="/to-words-converter">Convert money to words</Link>
        </ol>
      </div>
    );
  }
}
