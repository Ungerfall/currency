import { Home } from "./components/Home";
import ToWordsConverter from "./components/ToWordsConverter"

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/to-words-converter',
    element: <ToWordsConverter />
  }
];

export default AppRoutes;
