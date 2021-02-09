import { ContextualClass } from "../ContextualClass";
import { FaIcon } from "../FaIcon";
export interface ICommandItem {
    readonly executeRequested: IEventHandler<any>;
    readonly icon: FaIcon;
    positionIconRight(): any;
    setText(text: string): any;
    setTitle(text: string): any;
    setContext(contextualClass: ContextualClass): any;
    setActive(): any;
    setInactive(): any;
    show(): any;
    hide(): any;
    enable(): any;
    disable(): any;
}
