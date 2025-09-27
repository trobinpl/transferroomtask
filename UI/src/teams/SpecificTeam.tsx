import { type FC } from "react";

interface SpecificTeamProps {
	teamId: number;
}
export const SpecificTeam: FC<SpecificTeamProps> = ({ teamId }) => {
	return <div>Show team {teamId}</div>;
};
