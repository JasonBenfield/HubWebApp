import { Link } from "./Link";
import { LinkViewModel } from "./LinkViewModel";
import { TextSpan } from "./TextSpan";
export declare class TextLink extends Link {
    constructor(text?: string, vm?: LinkViewModel);
    readonly textSpan: TextSpan;
    setText(text: string): void;
}
