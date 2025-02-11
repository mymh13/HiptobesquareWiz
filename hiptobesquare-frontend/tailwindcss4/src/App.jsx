import { useState, useEffect } from 'react';

const API_URL = import.meta.env.VITE_API_URL;

const generateRandomColour = (prevColour) => {
    let colour;
    do {
        colour = `#${Math.floor(Math.random() * 16777215).toString(16).padStart(6, '0')}`.toUpperCase();
    } while (colour === prevColour);
    return colour;
};

function App() {
    const [squares, setSquares] = useState([]);

    // Fetch squares from API on page load
    useEffect(() => {
        fetch(API_URL)
            .then((response) => response.json())
            .then((data) => setSquares(data))
            .catch((error) => console.error("Error fetching squares:", error));
    }, []);

    const calculateNextPosition = (squares) => {
        const count = squares.length;
        if (count === 0) return { x: 0, y: 0 };

        let level = Math.floor(Math.sqrt(count));
        let x, y;

        if (count < (level + 1) ** 2 - level) {
            x = level;
            y = count - ((level) ** 2);
        } else {
            x = (level - 1) - (count - ((level + 1) ** 2 - level));
            y = level;
        }

        return { x, y };
    };

    const addSquare = async () => {
        const prevColour = squares.length > 0 ? squares[squares.length - 1].Colour : null;
        const { x, y } = calculateNextPosition(squares);

        const newSquare = {
            Id: `${x}-${y}`,
            Colour: generateRandomColour(prevColour),
            PositionX: x,
            PositionY: y,
        };

        console.log("Adding square:", newSquare);

        try {
            await fetch(API_URL, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(newSquare),
            });

            setSquares((prevSquares) => [...prevSquares, newSquare]);
            } catch (error) {
            console.error("Error saving square:", error);
        }
    };
    
    const clearSquares = async () => {
        await fetch(API_URL, { method: "DELETE" });
        setSquares([]);
    };

    return (
        <div className="container flex flex-col items-center min-h-screen px-6">
        {/* Top section, for top coders */}
            <div className="w-full max-w-4xl text-center py-6 px-8 bg-[#292929] rounded-xl shadow-lg">
                <a
                    href="https://mymh.dev/"
                    target="_blank"
                    rel="noopener noreferrer"
                    className="text-4xl font-bold transition-transform duration-200 
                    hover:scale-110 hover:text-[#00d0ff] hover:shadow-md mt-4 mb-6 block"
                >
                    hiptobesquare @ mymh.dev
                </a>

                {/* Buttons BUTTONS EVERYWHERE! */}
                <div className="mt-6 flex gap-8 justify-center border border-[#4ec9b0] px-4 py-4">
                    <button className="button" onClick={addSquare}>Add square</button>
                    <button className="button" onClick={clearSquares}>Clear</button>
                </div>
            </div>

            {/* Separator */}
            <div className="h-4"></div>

            {/* Squareing up */}
            <div className="relative w-full flex justify-center items-start p-4 bg-[#292929] rounded-xl shadow-lg mt-4"
                 style={{
                     height: `${Math.ceil(Math.sqrt(squares.length)) * 72}px`,
                     minHeight: "500px",
                 }}
            >
                <div className="relative">
                    {squares.map((square) => {
                        const gridWidth = Math.ceil(Math.sqrt(squares.length)) * 72;
                        return (
                            <div
                                key={square.Id}
                                className="absolute w-16 h-16 border border-black rounded-md"
                                style={{
                                    backgroundColor: square.Colour || "#ff0000",
                                    left: `calc(50% - ${gridWidth / 2}px + ${square.PositionX * 72}px)`,
                                    top: `${square.PositionY * 72}px`,
                                    transition: "transform 0.2s ease-in-out",
                                }}
                            />
                        );
                    })}
                </div>
            </div>
        </div>
    );
}

export default App;