import { Block } from './Block';
import { BlockViewModel } from './BlockViewModel';
export declare class Container extends Block {
    constructor(vm?: BlockViewModel);
    fluid(): void;
    private containerCss;
    private setContainerCss;
}
