import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import Jobs from './Jobs';
import registerServiceWorker from './registerServiceWorker';

ReactDOM.render(<Jobs />, document.getElementById('root'));
registerServiceWorker();
