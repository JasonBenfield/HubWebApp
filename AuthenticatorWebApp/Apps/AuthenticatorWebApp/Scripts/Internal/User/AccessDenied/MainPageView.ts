import { ContextualClass } from '@jasonbenfield/sharedwebapp/ContextualClass';
import { FlexCss } from '@jasonbenfield/sharedwebapp/FlexCss';
import { MarginCss } from '@jasonbenfield/sharedwebapp/MarginCss';
import { AlertView } from '@jasonbenfield/sharedwebapp/Views/AlertView';
import { BasicPageView } from '@jasonbenfield/sharedwebapp/Views/BasicPageView';
import { BasicTextComponentView } from '@jasonbenfield/sharedwebapp/Views/BasicTextComponentView';
import { BlockView } from '@jasonbenfield/sharedwebapp/Views/BlockView';
import { TextBlockView } from '@jasonbenfield/sharedwebapp/Views/TextBlockView';
import { TextHeading1View } from '@jasonbenfield/sharedwebapp/Views/TextHeadings';

export class MainPageView extends BasicPageView {
    readonly caption: BasicTextComponentView;
    readonly message: BasicTextComponentView;

    constructor() {
        super();
        const container = this.addView(BlockView);
        container.height100();
        let flexColumn = container.addView(BlockView);
        flexColumn.setFlexCss(new FlexCss().column())
        const flexFill = flexColumn.addView(BlockView)
            .configure(b => b.positionAbsoluteFill())
            .addView(BlockView)
            .configure(b => b.addCssName('container'));
        this.caption = flexFill.addView(TextHeading1View)
            .configure(h1 => h1.setMargin(MarginCss.top(3).xs({ bottom: 3 })));
        const alert = flexFill.addView(AlertView)
            .configure(a => a.setMargin(MarginCss.bottom(3)));
        alert.setContext(ContextualClass.danger);
        this.message = alert.addView(TextBlockView);
    }
}
