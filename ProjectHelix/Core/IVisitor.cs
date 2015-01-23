using ProjectHelix.Data;
using ProjectHelix.Data.BoatComponents;

namespace ProjectHelix.Core
{
    public interface IVisitor
    {
        void Visit(AbstractNode node);

        void Visit(CompositeNode node);

        void Visit(RenderingTree node);

        void Visit(Hull node);
        void Visit(Canons node);
        void Visit(Sails node);
        void Visit(Wheel node);
        void Visit(Ship node);
        void Visit(Canonball node);
    }
}
