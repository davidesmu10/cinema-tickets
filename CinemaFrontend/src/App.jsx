import Navbar from "./components/Navbar";
import Router from "./router.jsx";

export default function App() {
  return (
    <div className="min-h-screen">
      <Navbar />
      <div className="p-6">
        <Router />
      </div>
    </div>
  );
}
