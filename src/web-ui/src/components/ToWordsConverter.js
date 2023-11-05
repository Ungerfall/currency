import React, { useState } from 'react';

const ToWordsConverter = () => {
  const [amount, setAmount] = useState('');
  const [output, setOutput] = useState('');

  const handleInputChange = (e) => {
    setAmount(e.target.value);
  };

  const handleButtonClick = () => {
    fetch('/api/currencies/words', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ amount }),
    })
    .then(async (response) => {
      const data = await response.json();
      if (response.ok) {
        setOutput(data.words || 'No words received.');
      } else if (response.status === 400) {
        setOutput("Bad request: " + data.error || 'Bad request.');
      } else {
        throw new Error('Unhandled error');
      }
    })
    .catch((error) => {
      setOutput(error.message || 'An error occurred. Please try again or ask devs.');
    });
  };

  return (
    <div>
      <input type="text" value={amount} onChange={handleInputChange} />
      <button onClick={handleButtonClick}>Convert</button>
      <div>{output}</div>
    </div>
  );
};

export default ToWordsConverter;
