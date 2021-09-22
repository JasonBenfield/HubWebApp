import { ContextualClass } from './ContextualClass';
import { BlockViewModel } from './Html/BlockViewModel';
import { Block } from './Html/Block';
export declare class Alert extends Block {
    constructor(vm?: BlockViewModel);
    private context;
    setContext(context: ContextualClass): void;
}
