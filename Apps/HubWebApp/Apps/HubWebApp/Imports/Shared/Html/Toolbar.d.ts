import { Column } from "../Grid/Column";
import { BlockViewModel } from "./BlockViewModel";
import { HtmlComponent } from "./HtmlComponent";
export declare class Toolbar extends HtmlComponent {
    constructor(vm?: BlockViewModel);
    readonly columnStart: Column;
    readonly columnEnd: Column;
}
