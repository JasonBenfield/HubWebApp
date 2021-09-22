import { AsyncCommand } from './AsyncCommand';
export declare class Command extends AsyncCommand {
    constructor(action: (context?: any) => void);
}
