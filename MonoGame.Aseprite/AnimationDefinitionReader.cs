﻿//--------------------------------------------------------------------------------
//  AnimationDefinitionReader
//  Used by the ContentManger when using .Load<AnimationDefinition>() to allow
//  reading the AnimationDefinition .xnb file that was produced by the
//  contnet pipeline extension
//--------------------------------------------------------------------------------
//
//                              License
//  
//    Copyright(c) 2018 Chris Whitley
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the "Software"), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//--------------------------------------------------------------------------------
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace MonoGame.Aseprite
{
    public class AnimationDefinitionReader : ContentTypeReader<AnimationDefinition>
    {
        protected override AnimationDefinition Read(ContentReader input, AnimationDefinition existingInstance)
        {
            //  Read how many frames there are in total
            int frameCount = input.ReadInt32();

            //  Initialize a new list of frames
            List<Frame> frames = new List<Frame>();

            //  Read all the data about the frames
            for(int i = 0; i < frameCount; i++)
            {
                //  Read the x-coordinate value
                int x = input.ReadInt32();

                //  Read the y-coordinate value
                int y = input.ReadInt32();

                //  Read the (w)idth value
                int w = input.ReadInt32();

                //  Read the (h)eight value
                int h = input.ReadInt32();

                //  Read the duration value
                int duration = input.ReadInt32();

                //  Create a frame
                Frame frame = new Frame(x, y, w, h, duration);

                //  Store the frame
                frames.Add(frame);
            }

            //  Read how many animation definitions there are in total
            int animationCount = input.ReadInt32();

            //  Initilize a new dictionary for the animations
            Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

            //  Read all the animation definition data
            for(int i = 0; i < animationCount; i++)
            {
                //  Read the animation name
                string name = input.ReadString();

                //  Read the starting (from) frame
                int from = input.ReadInt32();

                //  Read the ending (to) frame
                int to = input.ReadInt32();

                //  Create a new animation definition
                Animation animation = new Animation(name, from, to);

                //  Store the animation
                animations.Add(animation.name, animation);
            }

            //  Create a new AnimationDefinition
            AnimationDefinition animationDefinition = new AnimationDefinition(animations, frames);

           
            return animationDefinition;
        }
    }
}