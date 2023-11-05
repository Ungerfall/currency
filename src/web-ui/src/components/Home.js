import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1>Currencies</h1>
        <h3>Features:</h3>
        <ol>
          <a href="/to-words-converter">Convert money to words</a>
        </ol>
      </div>
    );
  }
}
