export default function Seat({ seat, onSelect }) {
  return (
    <button
      disabled={seat.reserved}
      onClick={() => onSelect(seat)}
      className={`w-10 h-10 m-1 rounded 
        ${seat.reserved ? "bg-gray-400 cursor-not-allowed" : "bg-green-500 hover:bg-green-700"}
      `}
    >
      {seat.label}
    </button>
  );
}
