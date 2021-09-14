import { Block } from "./Block";
import { BlockViewModel } from "./BlockViewModel";
import { Input } from "./Input";
import { InputViewModel } from "./InputViewModel";
export declare class InputGroup extends Block {
    constructor(vm?: BlockViewModel);
    addInput(vm?: InputViewModel): Input;
}
