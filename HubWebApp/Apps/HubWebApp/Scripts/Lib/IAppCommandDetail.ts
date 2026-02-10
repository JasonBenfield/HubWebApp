import { App } from "./App";
import { AppCommand } from "./AppCommand";
import { AppCommandStep } from "./AppCommandStep";
import { InstallLocation } from "./InstallLocation";

export interface IAppCommandDetail {
    readonly command: AppCommand;
    readonly app: App;
    readonly location: InstallLocation;
    readonly steps: AppCommandStep[];
}