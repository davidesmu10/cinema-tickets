import { useParams, Link } from "react-router-dom";

export default function ConfirmationPage() {
  const { reservationId } = useParams();

  return (
    <div className="text-center bg-white p-8 shadow rounded max-w-lg mx-auto">
      <h1 className="text-2xl font-bold text-green-600">¡Reserva exitosa!</h1>
      <p className="mt-4">Tu código de reserva:</p>
      <p className="text-xl font-mono">{reservationId}</p>
      <Link to="/" className="mt-6 inline-block bg-blue-600 text-white px-4 py-2 rounded">
        Volver a cartelera
      </Link>
    </div>
  );
}
