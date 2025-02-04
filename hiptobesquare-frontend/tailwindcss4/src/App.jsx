// import reactLogo from './assets/react.svg' - ersätt detta med cool grafik senare!
//
import './App.css'

function App() {
    return (
        <div className="container flex flex-col items-center min-h-screen px-6">
        {/* Top section, for top people */}
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
                    <button className="bg-[#1a1a1a] text-[#4ec9b0] px-10 py-4 rounded-xl font-bold shadow-lg text-2xl
                    hover:bg-[#333333] transition transform hover:scale-105">
                        Add square
                    </button>
                    <button className="bg-[#1a1a1a] text-[#4ec9b0] px-10 py-4 rounded-xl font-bold shadow-lg text-2xl
                    hover:bg-[#333333] transition transform hover:scale-105">
                        Clear
                    </button>
                </div>
            </div>

            {/* Separator */}
            <div className="h-4"></div>

            {/* Lower section */}
            <div className="w-full max-w-4xl flex-grow flex flex-col items-center justify-start pt-6 bg-[#292929] rounded-xl shadow-lg mt-4">
                <p>Här kommer kvadraterna att läggas!</p>
            </div>
        </div>
    );
}

export default App;