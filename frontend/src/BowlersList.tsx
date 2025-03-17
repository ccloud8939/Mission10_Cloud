import { useEffect, useState } from "react";
import { bowler } from "./types/Bowler";
import { Team } from "./types/Team";

function BowlersList() {
    const [Bowlers, setBowlers] = useState<bowler[]>([]);
    const [Teams, setTeams] = useState<Map<number, Team>>(new Map()); // Store teams by teamId

    useEffect(() => {
        const fetchBowlers = async () => {
            const response = await fetch('http://localhost:4000/api/Bowlers');
            const data = await response.json();
            setBowlers(data);

            // Fetch teams after bowlers data is fetched
            const teamResponse = await fetch('http://localhost:4000/api/Teams'); // Assuming you have a teams endpoint
            const teamData = await teamResponse.json();
            const teamMap = new Map<number, Team>();
            teamData.forEach((team: Team) => teamMap.set(team.teamId, team));
            setTeams(teamMap);
        };

        fetchBowlers();
    }, []);

    // Filter bowlers to only show those in the "Marlins" or "Sharks" teams
    const filteredBowlers = Bowlers.filter(b => {
        const team = Teams.get(b.teamId);
        return team?.teamName === "Marlins" || team?.teamName === "Sharks";
    });


    return (
        <>
            <h1>Bowlers</h1>
            <table>
                <thead>
                    <tr>
                        <th>Full Name</th>
                        <th>Team Name</th>
                        <th>Address</th>
                        <th>City</th>
                        <th>State</th>
                        <th>Zip</th>
                        <th>Phone Number</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        filteredBowlers.map((b) => {
                            const team = Teams.get(b.teamId); // Get team by teamId
                            return (
                                <tr key={b.bowlerId}>
                                    <td>{b.bowlerFirstName} {b.bowlerMiddleInit} {b.bowlerLastName}</td>
                                    <td>{team ? team.teamName : "No Team"}</td>  {/* Display team name */}
                                    <td>{b.bowlerAddress}</td>
                                    <td>{b.bowlerCity}</td>
                                    <td>{b.bowlerState}</td>
                                    <td>{b.bowlerZip}</td>
                                    <td>{b.bowlerPhoneNumber}</td>
                                </tr>
                            );
                        })
                    }
                </tbody>
            </table>
        </>
    );
}


export default BowlersList;